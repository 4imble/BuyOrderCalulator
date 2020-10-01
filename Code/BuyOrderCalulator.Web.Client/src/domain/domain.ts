export class Item
{
    id: number  = 0;
    name: string  = "";
    unitPrice: number  = 0;
    quantity: number  = 0;
    reorderLevel: number  = 0;
    takingOrders: boolean  = true;

    typeId: number = 0;
    typeName: string = "";

    supplyTypeId: number = 0;
    supplyTypeName: string = "";
    pricePercentModifier: number = 0;
    corpCreditPercent: number  = 0;
}

export class SaleItem
{
    itemId: number = 0;
    quantity: number = 0;
}

export class Order
{
    id: number = 0;
    guid: string = "";
    orderItems: Array<OrderItem> = [];
    dateCreated: string = "";
    state: OrderStatus = OrderStatus.Open
}

export class OrderItem
{
    quantity: number = 0;
    fixedCorpCreditPercent: number = 0;
    fixedUnitPrice: number = 0;
    itemName: string = "";
    itemId: number = 0;
}

export enum OrderStatus
{
    Open,
    Complete,
    Cancelled
}

export class User
{
    name: string = "";
    token: string = "";
}