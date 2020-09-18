export class Item
{
    id: number  = 0;
    name: string  = "";
    unitPrice: number  = 0;
    quantity: number  = 0;
    reorderLevel: number  = 0;
    corpCreditMultiplier: number  = 0;
    takingOrders: boolean  = true;

    typeId: number = 0;
    typeName: string = "";

    supplyTypeId: number = 0;
    supplyTypeName: string = "";
}

export class SaleItem
{
    itemId: number = 0;
    quantity: number = 0;
}