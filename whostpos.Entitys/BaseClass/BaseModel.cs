using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace whostpos.Entitys.BaseClass
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public void OnPropertyChange<T>(Expression<Func<T>> expression)
        {
            var body = expression.Body as MemberExpression;
            string propertyName;
            if (body != null)
            {
                propertyName = ((MemberExpression)expression.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)expression.Body).Operand;
                propertyName = ((MemberExpression)op).Member.Name;
            }

            OnPropertyChange(propertyName);
        }
    }
}
