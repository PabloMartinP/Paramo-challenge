using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Api.Model
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }

        public string ErrorsToString()
        {
            if(Errors!=null)
                return string.Join(';', Errors);
                
            return "";
        }

    }
}
