class PlayerCoin{
    public string username {get; set;}
    public int coin {get; set;}

    public PlayerCoin(string username, int coin){
        this.username = username;
        this.coin = coin;
    }

    public string ToLine(){
        return this.username + " : " + this.coin;
    }

    public int Equals(PlayerCoin pc){
        if(this.coin < pc.coin) return -1;
        else if(this.coin > pc.coin) return 1;
        else return 0;
    }
}