export class Tentativa{
    constructor( 
        public cheio: boolean, 
        public urlCheio: string = "/assets/coracao_cheio.png",
        public urlVazio: string = "/assets/coracao_vazio.png"
    ) {}

    public exibeCoracao(): string{
        if(this.cheio){
            return this.urlCheio
        }else{
            return this.urlVazio
        }
    }
}