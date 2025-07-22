import java.util.Scanner;

class Vehicle {
    protected String make;
    protected String vehicleNumber;
    protected String fuelType;
    protected int fuelCapacity;
    protected int cc;

    public Vehicle(String make, String vehicleNumber, String fuelType, int fuelCapacity, int cc) {
        this.make = make;
        this.vehicleNumber = vehicleNumber;
        this.fuelType = fuelType;
        this.fuelCapacity = fuelCapacity;
        this.cc = cc;
    }

    public void displayMake() {
        System.out.println("\n*** " + make + " ***");
    }

    public void displayBasicInfo() {
        System.out.println("--- Basic Information ---");
        System.out.println("Vehicle Number: " + vehicleNumber);
        System.out.println("Fuel Type: " + fuelType);
        System.out.println("Fuel Capacity: " + fuelCapacity);
        System.out.println("CC: " + cc);
    }

    public void displayDetailInfo() {
        
    }

    public String getMake() { return make; }
    public void setMake(String make) { this.make = make; }

    public String getVehicleNumber() { return vehicleNumber; }
    public void setVehicleNumber(String vehicleNumber) { this.vehicleNumber = vehicleNumber; }

    public String getFuelType() { return fuelType; }
    public void setFuelType(String fuelType) { this.fuelType = fuelType; }

    public int getFuelCapacity() { return fuelCapacity; }
    public void setFuelCapacity(int fuelCapacity) { this.fuelCapacity = fuelCapacity; }

    public int getCc() { return cc; }
    public void setCc(int cc) { this.cc = cc; }
}

class TwoWheeler extends Vehicle {
    private boolean kickStartAvailable;

    public TwoWheeler(String make, String vehicleNumber, String fuelType, int fuelCapacity, int cc, boolean kickStartAvailable) {
        super(make, vehicleNumber, fuelType, fuelCapacity, cc);
        this.kickStartAvailable = kickStartAvailable;
    }

    @Override
    public void displayDetailInfo() {
        System.out.println("--- Detail Information ---");
        System.out.println("Kick Start Available: " + (kickStartAvailable ? "Yes" : "No"));
    }

    public boolean isKickStartAvailable() {
        return kickStartAvailable;
    }

    public void setKickStartAvailable(boolean kickStartAvailable) {
        this.kickStartAvailable = kickStartAvailable;
    }
}

class FourWheeler extends Vehicle {
    private String audioSystem;
    private int numberOfDoors;

    public FourWheeler(String make, String vehicleNumber, String fuelType, int fuelCapacity, int cc, String audioSystem, int numberOfDoors) {
        super(make, vehicleNumber, fuelType, fuelCapacity, cc);
        this.audioSystem = audioSystem;
        this.numberOfDoors = numberOfDoors;
    }

    @Override
    public void displayDetailInfo() {
        System.out.println("--- Detail Information ---");
        System.out.println("Audio System: " + audioSystem);
        System.out.println("Number of Doors: " + numberOfDoors);
    }

    public String getAudioSystem() {
        return audioSystem;
    }

    public void setAudioSystem(String audioSystem) {
        this.audioSystem = audioSystem;
    }

    public int getNumberOfDoors() {
        return numberOfDoors;
    }

    public void setNumberOfDoors(int numberOfDoors) {
        this.numberOfDoors = numberOfDoors;
    }
}

public class VehicleApp {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        Vehicle vehicle = null;

        System.out.println("1. Four Wheeler");
        System.out.println("2. Two Wheeler");
        System.out.print("Enter Vehicle Type (1 or 2): ");
        int choice = sc.nextInt();
        sc.nextLine(); 

        System.out.print("Vehicle Make: ");
        String make = sc.nextLine();
        System.out.print("Vehicle Number: ");
        String number = sc.nextLine();
        System.out.print("Fuel Type (Petrol/Diesel): ");
        String fuel = sc.nextLine();
        System.out.print("Fuel Capacity: ");
        int capacity = sc.nextInt();
        System.out.print("Engine CC: ");
        int cc = sc.nextInt();
        sc.nextLine(); 

        if (choice == 1) {
            System.out.print("Audio System: ");
            String audioSystem = sc.nextLine();
            System.out.print("Number of Doors: ");
            int doors = sc.nextInt();
            vehicle = new FourWheeler(make, number, fuel, capacity, cc, audioSystem, doors);
        } else if (choice == 2) {
            System.out.print("Kick Start Available (yes/no): ");
            String kick = sc.nextLine();
            boolean kickStart = kick.equalsIgnoreCase("yes");
            vehicle = new TwoWheeler(make, number, fuel, capacity, cc, kickStart);
        } else {
            System.out.println("Invalid choice.");
            return;
        }

        vehicle.displayMake();
        vehicle.displayBasicInfo();
        vehicle.displayDetailInfo();
    }
}
