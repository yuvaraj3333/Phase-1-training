class TemperatureConverter:
    @staticmethod
    def celsius_to_fahrenheit(celsius):
        return (celsius * 9/5) + 32

    @staticmethod
    def fahrenheit_to_celsius(fahrenheit):
        return (fahrenheit - 32) * 5/9

if __name__ == "__main__":
    print("25°C in Fahrenheit:", TemperatureConverter.celsius_to_fahrenheit(25))
    print("77°F in Celsius:", TemperatureConverter.fahrenheit_to_celsius(77))
