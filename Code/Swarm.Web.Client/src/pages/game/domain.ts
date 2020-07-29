
export enum TileColor
{
    Green,
    Orange,
    Unassigned
}

export class Game
{
    id: string;

    constructor(id: string)
    {
        this.id = id;
    }
}

export class Tile
{
    constructor(x: number, y: number, color: TileColor) {
        this.x = x;
        this.y = y;
        this.color = color;
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