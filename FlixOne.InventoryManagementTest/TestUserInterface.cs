using FlixOne.InventoryManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagementTest
{
    public class TestUserInterface : IUserInterface
    {
        private List<Tuple<string, string>> _expectedReadRequests;
        private List<string> _expectedWriteWarningRequestsIndex;
        private List<string> _expectedWriteWarningRequests;

        private int _expectedReadRequestsIndex = default;
        private int _expectedWriteWarningRequestIndex = default;

        public TestUserInterface(List<Tuple<string, string>> _expectedReadRequests, List<string> _expectedWriteWarningRequestsIndex
            , List<string> _expectedWriteWarningRequests)
        {
            this._expectedReadRequests = _expectedReadRequests;
            this._expectedWriteWarningRequestsIndex = _expectedWriteWarningRequestsIndex;
            this._expectedWriteWarningRequests = _expectedWriteWarningRequests;
        }

        public string ReadValue(string message)
        {
            Assert.IsTrue(_expectedReadRequestsIndex < _expectedReadRequests.Count, "Received too many command read requests.");
            Assert.AreEqual(_expectedReadRequests[_expectedReadRequestsIndex].Item1, message, "Received unexpected command read message.");
            return _expectedReadRequests[_expectedReadRequestsIndex].Item2;
        }

        public void WriteMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void WriteWarning(string message)
        {
            throw new NotImplementedException();
        }

        public void WriteWarnings(string message)
        {
            Assert.IsTrue(_expectedWriteWarningRequestIndex < _expectedWriteWarningRequestsIndex.Count, "Received too many command write warning requests.");
            Assert.AreEqual(_expectedWriteWarningRequests[_expectedWriteWarningRequestIndex++], message, "Received unexpected command write warning message.");
        }
    }
}
