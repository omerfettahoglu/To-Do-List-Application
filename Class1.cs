using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjToDoList
{
    class clsTask
    {
        string _sText;
        DateTime _dtDueDate;
        bool _bIsPriority;
        bool _bIsFinished;
        public clsTask(string sText, DateTime dtDueDate, bool bIsPriority, bool bIsFinished)
        {
            _sText = sText;
            _dtDueDate = dtDueDate;
            _bIsPriority = bIsPriority;
            _bIsFinished = bIsFinished;
        }
        public string sText { get { return _sText; } set { _sText = value; } }
        public DateTime dtDueDate { get { return _dtDueDate; } set { _dtDueDate = value; } }
        public bool bIsPriority { get { return _bIsPriority; } set { _bIsPriority = value; } }
        public bool bIsFinished { get { return _bIsFinished; } set { _bIsFinished = value; } }
    }
}