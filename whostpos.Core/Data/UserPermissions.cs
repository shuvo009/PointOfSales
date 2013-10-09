using System.Collections.ObjectModel;
using whostpos.Entitys.Entites;

namespace whostpos.Core.Data
{
    public class UserPermissions
    {
        public ObservableCollection<Permission> GetUserPermissions()
        {
            return new ObservableCollection<Permission>
                {
                    new Permission{PermissionType = "Setup Bank Accounts", IsAccessable = false},
                    new Permission{PermissionType = "View Bank Deposit Report", IsAccessable= false},
                    new Permission{PermissionType = "Do Bank Deposit", IsAccessable = false},
                    new Permission{PermissionType = "View Bank Withdrawal Report", IsAccessable = false},
                    new Permission{PermissionType = "Do Bank Withdrawal", IsAccessable = false},
                    new Permission{PermissionType = "View Cash Report", IsAccessable = false},
                    new Permission{PermissionType = "View Customer Balance", IsAccessable = false},
                    new Permission{PermissionType = "Customer Due Payment", IsAccessable = false},
                    new Permission{PermissionType = "View Customer Ledger", IsAccessable = false},
                    new Permission{PermissionType = "View Customer Report", IsAccessable = false},
                    new Permission{PermissionType = "Setup Customer", IsAccessable = false},
                    new Permission{PermissionType = "View Product Report", IsAccessable = false},
                    new Permission{PermissionType = "Setup Product", IsAccessable = false},
                    new Permission{PermissionType = "View Stock Register", IsAccessable = false},
                    new Permission{PermissionType = "View Stock", IsAccessable = false},
                    new Permission{PermissionType = "View Supplier Ledger", IsAccessable = false},
                    new Permission{PermissionType = "View Supplier Balance", IsAccessable = false},
                    new Permission{PermissionType = "Supplier Due Payment", IsAccessable = false},
                    new Permission{PermissionType = "Supplier Report", IsAccessable = false},
                    new Permission{PermissionType = "Setup Supplier", IsAccessable = false},
                    new Permission{PermissionType = "View Purchase Return Report", IsAccessable = false},
                    new Permission{PermissionType = "Purchase Return", IsAccessable = false},
                    new Permission{PermissionType = "View Sales Report", IsAccessable = false},
                    new Permission{PermissionType = "View Sales Return Report", IsAccessable = false},
                    new Permission{PermissionType = "View Sales Return", IsAccessable = false},
                    new Permission{PermissionType = "Sales", IsAccessable = false},
                    new Permission{PermissionType = "Purchase", IsAccessable = false},
                    new Permission{PermissionType = "View Expenses Report", IsAccessable = false},
                    new Permission{PermissionType = "View Database Restore And Backup", IsAccessable = false},
                    new Permission{PermissionType = "View Initial Balance", IsAccessable = false},
                    new Permission{PermissionType = "Do Expense", IsAccessable = false},
                };
        }
    }
}
