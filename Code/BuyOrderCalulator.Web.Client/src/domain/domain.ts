export class Item
{
    id: string  = "";
    name: string  = "";
    unitPrice: number  = 0;
    quantity: number  = 0;
    reorderLevel: number  = 0;
    reorderCreditValue: number  = 0;
    takingOrders: boolean  = true;

    typeId: number = 0;
    typeName: string = "";

    supplyTypeId: number = 0;
    supplyTypeName: string = "";
}