using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using whostpos.Core.Classes;
using whostpos.Core.Interface;
using whostpos.MenuCommands;
using whostpos.ViewModel;

namespace whostpos.Class
{
    public sealed class CommandParser
    {
        private readonly IEnumerable<ICommandFactory> _availableCommands;
        private static CommandParser _commandParser;
        public readonly List<string> GeneralCommands = new List<string>
        {
            "User Login","Activation"
        };
        private CommandParser()
        {
            _availableCommands = GetCommands();
        }

        public static CommandParser Parser
        {
            get { return _commandParser ?? (_commandParser = new CommandParser()); }
        }

        private ICommand ParseCommand(string requestedCommand, ContentPresenter contentPresenter)
        {
            if (!IsGeneral(requestedCommand))
            {
                if (!UserAuthorize.Authorize.IsAccessable(requestedCommand))
                    return new PermissionNotFound();
            }
            var command = FindRequestedCommand(requestedCommand);
            return command == null ? new NotFoundCommand { Name = requestedCommand } : command.MakeCommand(contentPresenter);
        }

        public void LoadFeture(string requestedCommand, ContentPresenter contentPresenter)
        {
            var command = ParseCommand(requestedCommand, contentPresenter);
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ViewModelLocator.Cleanup();
                command.Execute();
            }));
        }

        ICommandFactory FindRequestedCommand(string commandName)
        {
            return _availableCommands.FirstOrDefault(x => x.CommandName.Equals(commandName));
        }

        private IEnumerable<ICommandFactory> GetCommands()
        {
            return new ICommandFactory[]
                {
                    new SetupBankAccountsCommand(), 
                    new ViewBankDepositReportCommand(), 
                    new DoBankDepositCommand(), 
                    new ViewBankWithdrawalReportCommand(), 
                    new DoBankWithdrawalCommand(), 
                    new ViewCashReportCommand(), 
                    new ViewCustomerBalanceCommand(), 
                    new CustomerDuePaymentCommand(), 
                    new ViewCustomerLedgerCommand(), 
                    new ViewCustomerReportCommand(),
                    new SetupCustomerCommand(),
                    new ViewProductReportCommand(),
                    new SetupProductCommand(),
                    new ViewStockRegisterCommand(),
                    new ViewStockCommand(),
                    new ViewSupplierLedgerCommand(),
                    new ViewSupplierBalanceCommand(),
                    new SupplierDuePaymentCommand(),
                    new SupplierReportCommand(),
                    new SetupSupplierCommand(), 
                    new ViewPurchaseReturnReportCommand(),
                    new PurchaseReturnCommand(),
                    new ViewSalesReportCommand(),
                    new ViewSalesReturnReportCommand(),
                    new ViewSalesReturnCommand(),
                    new SalesCommand(),
                    new PurchaseCommand(), 
                    new CompanyCommand(), 
                    new ViewExpensesReport(),
                    new UserAccountMenuCommand(),
                    new ViewDatabaseRestoreAndBackupCommand(),
                    new UserLoginCommand(), 
                    new ViewInitialBalanceCommand(), 
                    new DoExpenseCommand(), 
                    new HomeCommand(), 
                    new ActivationCommand(), 
                };
        }

        private bool IsGeneral(string requestCommand)
        {
            return GeneralCommands.Any(x => x.Equals(requestCommand));
        }
    }
}
