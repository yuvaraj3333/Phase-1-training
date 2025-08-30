export interface AccountDetails {
  id: number;
  accountNumber: string;
  amount: number;
}

export interface BankDetails {
  id: number;
  name: string;
  age: number;
  username: string;
  accounts: AccountDetails[];
}
