using Newtonsoft.Json;
using SqlWebToDev;
using System.Collections.Generic;
using System.Linq;

namespace Common.Helper
{
    public static class AgGridHelper
    {
        public static string GetWhereSql(string filters)
        {
            List<FilterModel<object, object>> filterModels = new List<FilterModel<object, object>>();
            if (!string.IsNullOrEmpty(filters))
            {
                filterModels = JsonConvert.DeserializeObject<List<FilterModel<object, object>>>(filters);
            }
            string filterQuery = GetWhereSqlDynamic(filterModels);            
            if (!string.IsNullOrEmpty(filterQuery))
            {
                filterQuery = $" AND {filterQuery}";
            }
            filterQuery = filterQuery?.Replace("LIKE '%", "LIKE N'%");
            return filterQuery;
        }

        public static string GetWhereSqlDynamic(List<FilterModel<object, object>> filterModels)
        {
            string key = "";
            string type = "";
            object filter = "";
            string Operator = "";
            string filterQuery = "";
            string filterType = "";
            object filterCondition1 = "";
            object filterCondition2 = "";
            string filterConditionType1 = "";
            string filterConditionType2 = "";
            string filterConditionFilterType1 = "";
            string filterConditionFilterType2 = "";

            if (filterModels != null)
            {
                int autoincrement = 0;
                foreach (var item in filterModels.Where(x=>x.Filter!=null))
                {

                    filterType = item.FilterType;
                    filter = item.Filter;
                    type = item.Type;
                    key = item.Key;
                    Operator = item.Operator;
                    if (item.Condition1 != null && item.Condition2 != null)
                    {
                        filterCondition1 = item.Condition1.Filter;
                        filterCondition2 = item.Condition2.Filter;
                        filterConditionType1 = item.Condition1.Type;
                        filterConditionType2 = item.Condition2.Type;
                        filterConditionFilterType1 = item.Condition1.FilterType;
                        filterConditionFilterType2 = item.Condition2.FilterType;
                        filterQuery = GetWhereSqlDynamicCondition(key, filterConditionFilterType1, filterConditionType1, filterCondition1);
                        filterQuery += " " + Operator + " ";
                        filterQuery += GetWhereSqlDynamicCondition(key, filterConditionFilterType2, filterConditionType2, filterCondition2);
                    }
                    else
                    {
                        if (autoincrement > 0)
                        {
                            filterQuery += " AND ";
                            filterQuery += GetWhereSqlDynamicCondition(key, filterType, type, filter);
                        }
                        else
                        {
                            filterQuery += GetWhereSqlDynamicCondition(key, filterType, type, filter);
                        }

                    }
                    autoincrement++;
                }


            }

            else
            {
                filterQuery = "";
            }


            return filterQuery;

        }

        public static string GetWhereSqlDynamicCondition(string key, string conditionfilterType, string conditionType, object conditionfilter)
        {
            string filterQuery = "";
            if (conditionfilterType == "date" && conditionType == "equals")
            {
                conditionType = "contains";

            }
            if (conditionfilterType == "date" && conditionType == "notEqual")
            {
                conditionType = "notContains";

            }
            switch (conditionType)
            {
                case "contains":
                    filterQuery = key + " LIKE '%" + conditionfilter + "%'";
                    break;

                case "notContains":
                    filterQuery = key + " NOT LIKE '%" + conditionfilter + "%'";
                    break;

                case "equals":
                    filterQuery = key + " = " + "'" + conditionfilter + "'";
                    break;

                case "notEqual":
                    filterQuery = key + " != " + "'" + conditionfilter + "'";
                    break;

                case "startsWith":
                    filterQuery = key + " LIKE '" + conditionfilter + "%'";
                    break;

                case "endsWith":
                    filterQuery = key + " LIKE '%" + conditionfilter + "'";
                    break;

                case "greaterThan":
                    filterQuery = key + " > " + "'" + conditionfilter + "'";
                    break;

                case "lessThan":
                    filterQuery = key + " < " + "'" + conditionfilter + "'";
                    break;

                case "greaterThanOrEqual":
                    filterQuery = key + " >= " + "'" + conditionfilter + "'";
                    break;

                case "lessThanOrEqual":
                    filterQuery = key + " <= " + "'" + conditionfilter + "'";
                    break;
            }

            return filterQuery;
        }
    }
}
