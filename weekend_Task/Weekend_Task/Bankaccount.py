class BankAccount:
    def __init__(self, account_no, name, balance=0.0):
        self.__accountno = account_no
        self.__name = name
        self.__balance = balance

    def set_accountno(self, account_no):
        self.__accountno = account_no

    def get_accountno(self):
        return self.__accountno

    def set_name(self, name):
        self.__name = name

    def get_name(self):
        return self.__name

    def set_balance(self, balance):
        if balance >= 0:
            self.__balance = balance
        else:
            print("Balance cannot be negative.")

    def get_balance(self):
        return self.__balance

    def deposit(self, amount):
        if amount > 0:
            self.__balance += amount
            print(f"Deposited: ₹{amount}. New Balance: ₹{self.__balance}")
        else:
            print("Deposit amount must be positive.")

    def withdraw(self, amount):
        if amount > 0:
            if self.__balance >= amount:
                self.__balance -= amount
                print(f"Withdrawn: ₹{amount}. New Balance: ₹{self.__balance}")
            else:
                print("Insufficient balance.")
        else:
            print("Withdrawal amount must be positive.")

if __name__ == "__main__":
    acc = BankAccount("87546789", "Yuvaraj", 5000.0)
    print("Account No:", acc.get_accountno())
    print("Name:", acc.get_name())
    print("Balance: ₹", acc.get_balance())

    acc.deposit(2000)
    acc.withdraw(1000)

    acc.set_name("Vicky")
    print("Updated Name:", acc.get_name())
