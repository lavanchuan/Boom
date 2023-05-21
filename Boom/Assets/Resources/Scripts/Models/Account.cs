class Account{
    public string username {get; set;}
    public string password {get; set;}

    public Account(string username, string password){
        this.username = username;
        this.password = password;
    }

    public bool Equals(Account account){
        return this.username == account.username && this.password == account.password;
    }

    public string ToLine(){
        return this.username + " : " + this.password;
    } // ToString()
}