using FC_MVVC.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_MVVC.Services
{
    public class WeightConverter
    {
        public static WeightLog ConvertToKg(WeightLog log)
        {
            log.WeightValue = log.WeightValue * (float)0.45359237;
            return log;
        }

        public static WeightLog ConvertToLbs(WeightLog log)
        {
            log.WeightValue = log.WeightValue / (float)0.45359237;
            return log;
        }
    }
}
