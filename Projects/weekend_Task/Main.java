import java.util.List;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        PackageService service = new PackageServiceImpl();

        while (true) {
            System.out.println("\n1. Add Package details:");
            System.out.println("2. Display all package details:");
            System.out.println("3. Search for a package with package id:");
            System.out.println("4. Calculate package cost based on package id:");
            System.out.println("5. Exit:");
            System.out.print("Enter choice: ");
            int choice = sc.nextInt();
            sc.nextLine();

            try {
                switch (choice) {
                    case 1:
                        System.out.print("Package ID (7 chars): ");
                        String id = sc.nextLine();
                        System.out.print("Source place: ");
                        String source = sc.nextLine();
                        System.out.print("Destination place: ");
                        String dest = sc.nextLine();
                        System.out.print("Number of days: ");
                        int days = sc.nextInt();
                        System.out.print("Basic fare: ");
                        double fare = sc.nextDouble();
                        sc.nextLine();

                        Package pkg = new Package(id, source, dest, days, fare);
                        service.addPackage(pkg);
                        System.out.println("Package added successfully!");
                        break;

                    case 2:
                        List<Package> all = service.fetchAllPackages();
                        if (all.isEmpty()) System.out.println("No packages found.");
                        else all.forEach(System.out::println);
                        break;

                    case 3:
                        System.out.print("Enter package ID to search: ");
                        String sid = sc.nextLine();
                        Package p = service.findPackageById(sid);
                        if (p == null) System.out.println("Package not found.");
                        else System.out.println(p);
                        break;

                    case 4:
                        System.out.print("Enter package ID to calculate cost: ");
                        String cid = sc.nextLine();
                        service.calculatePackageCost(cid);
                        System.out.println("Package cost calculated.");
                        break;

                    case 5:
                        System.out.println("Exiting...");
                        sc.close();
                        System.exit(0);

                    default:
                        System.out.println("Invalid choice.");
                }
            } catch (InvalidPackageIdException e) {
                System.out.println("Error: " + e.getMessage());
            } catch (Exception e) {
                System.out.println("Error: " + e.getMessage());
            }
        }
    }
}
