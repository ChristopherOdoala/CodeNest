using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface ICurrencyConvertionService
    {
        ResultModel ConvertNairaToDollar(double amount, int currencyId);
        double ConvertNairaToPounds(double amount, int currencyId);
        double ConvertNairaToRands(double amount, int currencyId);
        List<ValidationResult> AddRate(Rate model);
    }

    public class CurrencyConvertionService : ICurrencyConvertionService
    {
        private DataContext _dataContext;
        public CurrencyConvertionService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Rate> GetAllRates()
        {
            return _dataContext.Rates;
        }

        public Rate GetRateById(int Id)
        {
            return GetAllRates().Where(x => x.Id == Id).FirstOrDefault();
        }

        public List<ValidationResult> AddRate(Rate model)
        {
            var errorlist = new List<ValidationResult> { };
            try
            {

                _dataContext.Rates.Add(model);
                _dataContext.SaveChanges();
            }
            catch(Exception ex)
            {
                errorlist.Add(new ValidationResult(ex.Message));
                return errorlist;
            }
            return errorlist;
        }

        public ResultModel ConvertNairaToDollar(double amount, int currencyId)
        {
            var errorlist = new List<ValidationResult> { };
            var resultModel = new ResultModel();
            if(amount == 0)
            {
                resultModel.errorList.Add(new ValidationResult("Amount cannot be Zero"));
                return resultModel;
            }

            if (currencyId == 0)
            {
                resultModel.errorList.Add(new ValidationResult("currency id must not be null"));
                return resultModel;
            }

            var currency = GetRateById(currencyId);
            if(currency is null)
            {
                resultModel.errorList.Add(new ValidationResult("No currency value found"));
                return resultModel;
            }
            var rate = currency.Percentage;
            var newAmount = amount / rate;
            resultModel.newAmount = newAmount;
            return resultModel;

        }

        public double ConvertNairaToPounds(double amount, int currencyId)
        {
            throw new NotImplementedException();
        }

        public double ConvertNairaToRands(double amount, int currencyId)
        {
            throw new NotImplementedException();
        }
    }
}
