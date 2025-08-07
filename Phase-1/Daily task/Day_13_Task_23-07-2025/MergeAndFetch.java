import java.util.ArrayList;
import java.util.Collections;
import java.util.Scanner;

public class MergeAndFetch {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        ArrayList<Integer> list1 = new ArrayList<>();
        ArrayList<Integer> list2 = new ArrayList<>();

        System.out.println("Enter 5 integers for the first list:");
        for (int i = 0; i < 5; i++) {
            list1.add(scanner.nextInt());
        }

        System.out.println("Enter 5 integers for the second list:");
        for (int i = 0; i < 5; i++) {
            list2.add(scanner.nextInt());
        }

        ArrayList<Integer> result = mergeSortAndFetch(list1, list2);
        System.out.println("Final ArrayList with elements at indices 2, 6, and 8: " + result);

        scanner.close();
    }

    public static ArrayList<Integer> mergeSortAndFetch(ArrayList<Integer> list1, ArrayList<Integer> list2) {
        ArrayList<Integer> mergedList = new ArrayList<>();
        mergedList.addAll(list1);
        mergedList.addAll(list2);

        Collections.sort(mergedList);

        ArrayList<Integer> finalList = new ArrayList<>();
        int[] indices = {2, 6, 8};

        for (int index : indices) {
            if (index < mergedList.size()) {
                finalList.add(mergedList.get(index));
            } else {
                System.out.println("Index " + index + " is out of bounds.");
            }
        }

        return finalList;
    }
}
