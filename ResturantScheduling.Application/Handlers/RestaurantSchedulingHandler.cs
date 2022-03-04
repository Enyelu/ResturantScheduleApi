using ResturantScheduling.Application.Commands;
using ResturantScheduling.Application.Responses;
using ResturantScheduling.Domain.Entities;

namespace ResturantScheduling.Application.Handlers
{
    public class RestaurantSchedulingHandler : IRequestHandler<OpenCloseRequest, OpenCloseResponse>
    {
        /// <summary>  
        /// this action get the UTC time from the unix time 
        /// </summary>  
        public static string CalculateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime.ToShortTimeString();
        }

        /* public static string CalculateTime(double unixTimeStamp)
         {
             DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
             var stringResult = dateTime.AddSeconds(unixTimeStamp).ToLocalTime().ToString();

             return stringResult.Substring(9);
         }*/

        public Task<OpenCloseResponse> Handle(OpenCloseRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <summary>  
        /// this handles the converstion for all the model
        /// </summary> 
        public OpenCloseResponse Converter(OpenCloseRequest requests)
        {
            return new OpenCloseResponse
            {
                Sunday = string.Format("Sunday: {0}", PrintTime(TimeSorter(requests.Sunday, requests.Saturday, requests.Monday, "Saturday", "Monday"))),
                Monday = string.Format("Monday: {0}", PrintTime(TimeSorter(requests.Monday, requests.Sunday, requests.Tuesday, "Sunday", "Tuesday"))),
                Tuesday = string.Format("Tuesday: {0}", PrintTime(TimeSorter(requests.Tuesday, requests.Monday, requests.Wednesday, "Monday", "Wednesday"))),
                Wednesday = string.Format("Wednesday: {0}", PrintTime(TimeSorter(requests.Wednesday, requests.Tuesday, requests.Thursday, "Tuesday", "Thursday"))),
                Thursday = string.Format("Thursday: {0}", PrintTime(TimeSorter(requests.Thursday, requests.Wednesday, requests.Friday, "Wednesday", "Friday"))),
                Friday = string.Format("Friday: {0}", PrintTime(TimeSorter(requests.Friday, requests.Thursday, requests.Saturday, "Thursday", "Saturday"))),
                Saturday = string.Format("Saturday: {0}", PrintTime(TimeSorter(requests.Saturday, requests.Friday, requests.Sunday, "Friday", "Sunday"))),
            };
        }

        /// <summary>  
        /// get the time readable timestamp
        /// </summary> 

        public List<OpenCloseTime> TimeSorter(List<OpenClose> openHourModel, List<OpenClose> previousDay, List<OpenClose> nextDay, string previousDayAsWord, string nextDayAsWord)
        {
            var times = new List<OpenCloseTime>();

            if (openHourModel == null)
            {
                return times;
            }

            var allitems = openHourModel.OrderBy(x => x.Value).ToList();
            var openhours = openHourModel.OrderBy(x => x.Type).Where(x => x.Type.ToLower() == "open").ToList();
            var closehours = openHourModel.OrderBy(x => x.Type).Where(x => x.Type.ToLower() == "close").ToList();
            if ((nextDay != null) && (nextDay.Any()))
            {
                nextDay = nextDay.OrderBy(x => x.Value).ToList();
            }

            if ((previousDay != null) && (previousDay.Any()))
            {
                previousDay = previousDay.OrderBy(x => x.Value).ToList();
            }
            if (allitems.Count < 1)
            {
                return times;
            }
            if ((nextDay != null) && (nextDay.Any()))
            {
                if (allitems.LastOrDefault().Type.ToLower() == "open" && (nextDay.Count > 0 && nextDay.FirstOrDefault().Type.ToLower() == "close"))
                {
                    var time = new OpenCloseTime
                    {
                        OpenTime = CalculateTime(allitems.LastOrDefault().Value),
                        CloseTime = string.Format("{0} {1}", nextDayAsWord, CalculateTime(nextDay.FirstOrDefault().Value))
                    };
                    times.Add(time);
                }
            }
            if (openhours.Count >= 1 && closehours.Count >= 1)
            {

                foreach (var closeitemhour in closehours)
                {
                    var openitemhour = openhours.Where(x => x.Value < closeitemhour.Value)
                                                .Select(y => y.Value).FirstOrDefault();

                    if (openitemhour != 0)
                    {
                        var time = new OpenCloseTime
                        {
                            OpenTime = CalculateTime(openitemhour),
                            CloseTime = CalculateTime(closeitemhour.Value)
                        };
                        times.Add(time);
                        
                        closehours.Remove(closeitemhour);
                        break;
                    }
                }
            }
            return times;
        }
        /// <summary>  
        /// Prints the time stamp for easy read.
        /// </summary> 
        public string PrintTime(List<OpenCloseTime> timer)
        {

            if (timer == null || timer.Count < 1) return "Closed";
            if (timer.Count == 1) return string.Format("{0} - {1}", timer.First().OpenTime, timer.First().CloseTime);
            else
            {
                var value = "";
                foreach (var item in timer)
                {
                    value = string.Format("{0} - {1},", item.OpenTime, item.CloseTime);
                }
                return value;

            }
        }

       
    }
}