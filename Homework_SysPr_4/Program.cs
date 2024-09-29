namespace Homework_SysPr_4
{
    public class Bank
    {
        private int accountBalance = 1000;

        public int GetBalance() => accountBalance;
        public void WithdrawMoney(int amount)
        {
            lock (this)
            {
                if (amount > accountBalance)
                {
                    Console.WriteLine("Insufficient funds.");
                }
                else
                {
                    Thread.Sleep(1000);
                    accountBalance -= amount;
                    Console.WriteLine("Successfully withdrew ${0}. New balanse: ${1}", amount, accountBalance);
                }
            }
        }
    }

    public class ATM
    {
        private Bank bank;
        public ATM(Bank bank)
        {
            this.bank = bank;
        }

        public void WithdrawMoney(int amount)
        {
            bank.WithdrawMoney(amount);
        }
    }

    public class Program
    {
        static void Main()
        {
            Bank bank = new Bank();
            ATM atm1 = new ATM(bank);
            ATM atm2 = new ATM(bank);

            new Thread(() => atm1.WithdrawMoney(500)).Start();
            new Thread(() => atm2.WithdrawMoney(750)).Start();

            Console.WriteLine($"Balance: {bank.GetBalance()}");
            Console.ReadLine();
        }
    }
}
