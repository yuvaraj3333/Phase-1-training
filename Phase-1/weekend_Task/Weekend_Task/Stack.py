class Stack:
    def __init__(self):
        self.items = []

    def is_empty(self):
        return len(self.items) == 0

    def push(self, item):
        self.items.append(item)
        print(f"Pushed {item} to stack.")

    def pop(self):
        if self.is_empty():
            return "Stack Underflow! Cannot pop from empty stack."
        return self.items.pop()

    def peek(self):
        if self.is_empty():
            return "Stack is empty!"
        return self.items[-1]

    def size(self):
        return len(self.items)

    def display(self):
        if self.is_empty():
            print("Stack is empty!")
        else:
            print("Stack (top to bottom):", list(reversed(self.items)))


if __name__ == "__main__":
    stack = Stack()

    stack.push(10)
    stack.push(20)
    stack.push(30)

    stack.display()         

    print("Top element:", stack.peek())  
    print("Popped:", stack.pop())        
    print("Stack size:", stack.size())  
    stack.display()                     
