import java.util.ArrayList;
import java.util.Scanner;

public class EvenOddList {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        ArrayList<Integer> inputList = new ArrayList<>();
        ArrayList<Integer> evenNumbersList = new ArrayList<>();
        ArrayList<Integer> oddNumbersList = new ArrayList<>();

        System.out.print("Enter number of elements: ");
        int n = scanner.nextInt();

        for (int i = 0; i < n; i++) {
            System.out.print("Enter integer " + (i + 1) + ": ");
            int num = scanner.nextInt();
            inputList.add(num);

            if (num % 2 == 0) {
                evenNumbersList.add(num);
            } else {
                oddNumbersList.add(num);
            }
        }

        System.out.println("\nInput List: " + inputList);
        System.out.println("Even Numbers List: " + evenNumbersList);
        System.out.println("Odd Numbers List: " + oddNumbersList);

        scanner.close();
    }
}
