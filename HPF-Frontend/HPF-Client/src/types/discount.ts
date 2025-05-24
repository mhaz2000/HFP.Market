
export interface Discount {
    id: string;
    code: string;
    name: string;
    maxAmount: number | null;
    startDate: string;
    endDate: string;
    usageLimitPerUser: number;
    percentage: number;
}


export interface UpdateDiscount extends Discount {
    id: string;
 }

export interface CreateDiscount extends Discount { }

export interface ApplyDiscount {
    buyerId: string,
    code: string
}

export interface AppliedDiscount {
    newPrice: number
}

