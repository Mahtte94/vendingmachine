

namespace VendingMachine
{
    public class Bank
    {
        private decimal _balance;

        public Bank(decimal initialBalance = 0)
        {
            _balance = initialBalance;
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }
            
            _balance += amount;
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || amount > _balance)
            {
                return false;
            }
            _balance -= amount;
            return true;
        }

        public decimal GetBalance()
        {
            return _balance;
        }
    }
}