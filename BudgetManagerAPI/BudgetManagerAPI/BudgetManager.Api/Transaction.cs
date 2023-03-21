namespace BudgetManager.Api
{
    public class Transaction 
    {
        public int ID {get; set;}
        public string Reference {get; set;}
        public decimal Amount {get; set;}
        public DateTime Date {get; set;}
        public string Comment {get; set;} 
        public string TransactionType {get; set;}
        public int WalletID {get; set;}
        public int CategoryID {get; set;}

    }
}