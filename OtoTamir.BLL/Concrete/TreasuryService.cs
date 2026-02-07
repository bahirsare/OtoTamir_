using AutoMapper;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using OtoTamir.CORE.Utilities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class TreasuryService : ITreasuryService
    {
        private readonly ITreasuryDal _treasuryDal;
        private readonly IBankDal _bankDal;
        private readonly IBankCardDal _bankCardDal;
        private readonly ITreasuryTransactionDal _transactionDal;
        private readonly IClientDal _clientDal;
        private readonly IPosTerminalDal _posTerminalDal;

        private readonly IMapper _mapper;

        public TreasuryService(
            ITreasuryDal treasuryDal,
            IBankDal bankDal,
            IBankCardDal bankCardDal,
            ITreasuryTransactionDal transactionDal,
            IClientDal clientDal,
            IPosTerminalDal posTerminalDal,
            IMapper mapper)
        {
            _treasuryDal = treasuryDal;
            _bankDal = bankDal;
            _bankCardDal = bankCardDal;
            _transactionDal = transactionDal;
            _clientDal = clientDal;
            _posTerminalDal = posTerminalDal;

            _mapper = mapper;
        }

        public Task<bool> AnyAsync(Expression<Func<Treasury, bool>> filter)
        {
            return _treasuryDal.AnyAsync(filter);
        }

        public Task<int> CreateAsync(Treasury entity)
        {
            return _treasuryDal.CreateAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _treasuryDal.DeleteAsync(id);
        }

        public Task<List<Treasury>> GetAllAsync(string mechanicId, int treasuryId, Expression<Func<Treasury, bool>> filter = null)
        {
            return _treasuryDal.GetAllAsync(mechanicId, treasuryId, filter);
        }



        public Task<Treasury> GetOneAsync(int id, string mechanicId)
        {
            return _treasuryDal.GetOneAsync(id, mechanicId);
        }

        public Task<decimal> GetTotalBalanceAsync(int treasuryId, string mechanicId)
        {
            return _treasuryDal.GetTotalBalanceAsync(treasuryId, mechanicId);
        }

        public Task<int> UpdateAsync()
        {
            return _treasuryDal.UpdateAsync();
        }
        public async Task<TreasuryDashboardDTO> GetDashboardDataAsync(string mechanicId, int treasuryId)
        {
            var model = new TreasuryDashboardDTO();

           
            var treasury = await _treasuryDal.GetOneAsync(treasuryId, mechanicId);
            if (treasury == null) return new TreasuryDashboardDTO { Treasury = new Treasury() };
            model.Treasury = treasury;

         
            var banks = await _bankDal.GetAllAsync(mechanicId);
            model.Banks = banks;
            var rawCards = await _bankCardDal.GetAllAsync(mechanicId);
            var cardSummaryList = new List<BankCardSummaryDTO>();
            var today = DateTime.Today;

            foreach (var card in rawCards)
            {
             
                var cardDto = _mapper.Map<BankCardSummaryDTO>(card);

               
                var linkedBank = banks.FirstOrDefault(b => b.Id == card.BankId);
                cardDto.BankName = linkedBank != null ? linkedBank.BankName : "-";

             
                cardDto.UsagePercent = card.Limit > 0 ? (card.Debt / card.Limit) * 100 : 0;
                cardDto.RemainingLimit = card.Limit - card.Debt;

                int rawBillingDay = (card.BillingDay <= 0) ? 1 : (card.BillingDay > 31 ? 31 : card.BillingDay);
                int rawDueDay = (card.DueDay <= 0) ? 1 : (card.DueDay > 31 ? 31 : card.DueDay);
                int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                
                int safeCutOffDay = Math.Min(card.BillingDay, daysInMonth);
                int safeDueDay = Math.Min(card.DueDay, daysInMonth);

                var nextCutOff = new DateTime(today.Year, today.Month, safeCutOffDay);
                var nextPayment = new DateTime(today.Year, today.Month, safeDueDay);

               
                if (nextCutOff < today)
                {
                    var nextM = today.AddMonths(1);
                    int safeDay = Math.Min(card.BillingDay, DateTime.DaysInMonth(nextM.Year, nextM.Month));
                    nextCutOff = new DateTime(nextM.Year, nextM.Month, safeDay);
                }

                if (nextPayment < today)
                {
                    var nextM = today.AddMonths(1);
                    int safeDay = Math.Min(card.DueDay, DateTime.DaysInMonth(nextM.Year, nextM.Month));
                    nextPayment = new DateTime(nextM.Year, nextM.Month, safeDay);
                }

                
                if (nextPayment < nextCutOff)
                {
                    var nextM = nextPayment.AddMonths(1);
                    int safeDay = Math.Min(card.DueDay, DateTime.DaysInMonth(nextM.Year, nextM.Month));
                    nextPayment = new DateTime(nextM.Year, nextM.Month, safeDay);
                }

                bool isCutoffCloser = (nextCutOff - today).Days < (nextPayment - today).Days;
                var criticalDate = isCutoffCloser ? nextCutOff : nextPayment;

            
                cardDto.CriticalDateDisplay = criticalDate.ToString("dd.MM.yyyy");
                cardDto.DateLabel = isCutoffCloser ? "Kesim" : "Ödeme";
                cardDto.IsAlert = (criticalDate - today).TotalDays <= 3;

                cardSummaryList.Add(cardDto);
            }

            model.BankCards = cardSummaryList; 

           
            var transactions = await _transactionDal.GetAllAsync(mechanicId, treasury.Id);
            model.Transactions = transactions.OrderByDescending(x => x.TransactionDate).Take(10).ToList();

           
            var clients = await _clientDal.GetAllAsync(mechanicId, false, false, c => c.Balance > 0);
            model.Treasury.ReceivablesBalance = clients.Sum(c => c.Balance);

           
            var posTerminals = await _posTerminalDal.GetAllAsync(mechanicId);
            var posTransactions = await _transactionDal.GetAllAsync(mechanicId, (int)treasuryId,
                x => x.PosTerminalId != null && x.TransactionType == TransactionType.Incoming);

            var posSummaryList = new List<PosTerminalSummaryDTO>();
            var lastMonthStart = DateTime.Now.AddMonths(-1).Date; 

            foreach (var pos in posTerminals)
            {
                var summaryItem = _mapper.Map<PosTerminalSummaryDTO>(pos);
                var myTrans = posTransactions.Where(t => t.PosTerminalId == pos.Id).ToList();
                var blockedList = myTrans.Where(t => t.MaturityDate != null && t.MaturityDate > DateTime.Now).ToList();

                summaryItem.BlockedBalance = blockedList.Sum(t => t.Amount);
                summaryItem.BlockedTransactionCount = blockedList.Count;

                var lastMonthList = myTrans.Where(t => t.TransactionDate >= lastMonthStart).ToList();

                summaryItem.LastMonthTurnover = lastMonthList.Sum(t => t.Amount);
                summaryItem.LastMonthTransactionCount = lastMonthList.Count;

                posSummaryList.Add(summaryItem);
            }

            model.PosTerminals = posSummaryList;
            return model;
        }
        public async Task UpdateCashBalanceAsync(int treasuryId, string mechanicId, decimal amount)
        {
            await _treasuryDal.UpdateCashBalanceAsync(treasuryId, mechanicId, amount);
        }

        Task<PagedResult<Treasury>> IRepositoryService<Treasury>.GetPagedAsync(Expression<Func<Treasury, bool>> filter, Func<IQueryable<Treasury>, IOrderedQueryable<Treasury>> orderBy, int page, int pageSize, params Expression<Func<Treasury, object>>[] includes)
        {
            return _treasuryDal.GetPagedAsync(filter, orderBy, page, pageSize, includes);
        }
    }
}