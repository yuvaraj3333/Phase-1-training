import java.util.HashMap;
import java.util.Scanner;

public class StudentGrades {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        HashMap<String, Float> studentMap = new HashMap<>();

        System.out.print("Enter number of students: ");
        int n = scanner.nextInt();
        scanner.nextLine(); 

        for (int i = 0; i < n; i++) {
            System.out.print("Enter student name: ");
            String name = scanner.nextLine();

            System.out.print("Enter mark for " + name + ": ");
            float mark = scanner.nextFloat();
            scanner.nextLine(); 

            studentMap.put(name, mark);
        }

        System.out.print("Enter student name to get grade: ");
        String searchName = scanner.nextLine();

        if (studentMap.containsKey(searchName)) {
            float mark = studentMap.get(searchName);
            if (mark >= 60) {
                System.out.println("Grade: PASS");
            } else {
                System.out.println("Grade: FAIL");
            }
        } else {
            System.out.println("Student not found.");
        }

        scanner.close();
    }
}
