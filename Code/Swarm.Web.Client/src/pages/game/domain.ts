
export enum TileColor
{
    Unassigned,
    Green,
    Orange
}

export class Game
{
    id: string  = "";
    player1: Player = new Player();
    player2: Player = new Player();
    pieces: Array<Tile> = [];
}

export class Player
{
    id: string = "";
    colour: TileColor = TileColor.Unassigned;
    name: string = "";
}

export class Tile
{
    constructor(x: number, y: number) {
        this.x = x;
        this.y = y;
        this.color = TileColor.Unassigned;
    }

    x: number = 0;
    y: number = 0;
    color: TileColor = TileColor.Unassigned;

    get edges(): Array<ICoOrds> {
        let top = { x: this.x, y: this.y - 2 };
        let bottom = { x: this.x, y: this.y + 2 };
        let lefts = [{ x: this.x - 1, y: this.y + 1 }, { x: this.x - 1, y: this.y - 1 }];
        let rights = [{ x: this.x + 1, y: this.y + 1 }, { x: this.x + 1, y: this.y - 1 }];
    
        let all = [];
        all.push(top, bottom, lefts, rights);
    
        return all.flat();
      }
}

export interface ICoOrds {
    x: number;
    y: number;
}