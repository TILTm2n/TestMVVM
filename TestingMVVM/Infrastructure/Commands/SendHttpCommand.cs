using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMVVM.Infrastructure.Commands.Base;

namespace TestingMVVM.Infrastructure.Commands
{
    class SendHttpCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            // какое-то действие
        }
    }
}
