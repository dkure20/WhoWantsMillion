using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWantsMillione
{
    public interface IQuestion
    {
        bool IsValidAnswer(string answer);
        void ShuffleAndPrintAnswers(List<string> answerList);
    }
}
