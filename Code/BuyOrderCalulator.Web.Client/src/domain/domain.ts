export class Item {
    id: number = 0;
    name: string = "";
    unitPrice: number = 0;
    quantity: number = 0;
    reorderLevel: number = 0;
    isActive: boolean = true;

    typeId: number = 0;
    typeName: string = "";

    supplyTypeId: number = 0;
    supplyTypeName: string = "";
    pricePercentModifier: number = 0;
    corpCreditPercent: number = 0;
}

export class CommonData {
    itemTypes: Array<ItemType> = [];
    supplyTypes: Array<SupplyType> = [];
}

export class ItemType {
    id: number = 0;
    name: string = "";
}

export class SupplyType {
    id: number = 0;
    name: string = "";
    pricePercentModifier: number = 0;
    corpCreditPercent: number = 0;
}

export class SaleItem {
    itemId: number = 0;
    quantity: number = 0;
}

export class Order {
    id: number = 0;
    guid: string = "";
    orderItems: Array<OrderItem> = [];
    dateCreated: string = "";
    state: OrderStatus = OrderStatus.Open;
    userNameDisplay: string = "";
    userAvatarLink: string = "";
}

export class OrderItem {
    quantity: number = 0;
    fixedCorpCreditPercent: number = 0;
    fixedUnitPrice: number = 0;
    itemName: string = "";
    itemId: string = "";
}

export enum OrderStatus {
    Open,
    Complete,
    Cancelled
}

export class User {
    discordId: string = "";
    userName: string = "";
    avatar: string = "";
    discriminator: string = "";
    isAdmin: string = "";
    accessToken: boolean = false;
    avatarLink: string = "";
    tokenExpires: Date = new Date();
}