import java.util.Scanner;

abstract class Shape {
    public abstract double calculateArea(); 
}

class Square extends Shape {
    private double side;

    public Square(double side) {
        this.side = side;
    }

    @Override
    public double calculateArea() {
        return side * side;
    }
}

class Rectangle extends Shape {
    private double length, width;

    public Rectangle(double length, double width) {
        this.length = length;
        this.width = width;
    }

    @Override
    public double calculateArea() {
        return length * width;
    }
}

class Triangle extends Shape {
    private double base, height;

    public Triangle(double base, double height) {
        this.base = base;
        this.height = height;
    }

    @Override
    public double calculateArea() {
        return 0.5 * base * height;
    }
}

public class ShapeAreaDemo {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        Shape shape;

        System.out.println("1. Square");
        System.out.println("2. Rectangle");
        System.out.println("3. Triangle");
        System.out.print("Choose a shape (1-3): ");
        int choice = sc.nextInt();

        switch (choice) {
            case 1:
                System.out.print("Enter side of square: ");
                double side = sc.nextDouble();
                shape = new Square(side);
                break;
            case 2:
                System.out.print("Enter length of rectangle: ");
                double length = sc.nextDouble();
                System.out.print("Enter width of rectangle: ");
                double width = sc.nextDouble();
                shape = new Rectangle(length, width);
                break;
            case 3:
                System.out.print("Enter base of triangle: ");
                double base = sc.nextDouble();
                System.out.print("Enter height of triangle: ");
                double height = sc.nextDouble();
                shape = new Triangle(base, height);
                break;
            default:
                System.out.println("Invalid choice.");
                return;
        }

        System.out.println("Area of the shape: " + shape.calculateArea());
    }
}
