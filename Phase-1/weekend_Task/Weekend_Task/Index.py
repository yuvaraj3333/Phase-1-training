class MyTuple:
    def __init__(self, values):
        self.values = values

    def __getitem__(self, index):
        return self.values[index]

t = MyTuple((1, 2, 3))
print(t[1])  
