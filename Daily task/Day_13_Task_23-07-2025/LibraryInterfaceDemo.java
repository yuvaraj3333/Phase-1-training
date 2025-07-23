
interface LibraryUser {

    void registerAccount();

    void requestBook(String bookType);
}

class KidUser implements LibraryUser {

    int age;
    String bookType;

    public KidUser(int age, String bookType) {
        this.age = age;
        this.bookType = bookType;
    }

    @Override
    public void registerAccount() {
        if (age < 12) {
            System.out.println("You have successfully registered under a Kids Account.");
        } else {
            System.out.println("Sorry, Age must be less than 12 to register as a kid.");
        }
    }

    @Override
    public void requestBook(String bookType) {
        if ("Kids".equalsIgnoreCase(bookType)) {
            System.out.println("Book Issued successfully, please return the book within 10 days.");
        } else {
            System.out.println("You are allowed to take only kids books.");
        }
    }
}

class AdultUser implements LibraryUser {

    int age;
    String bookType;

    public AdultUser(int age, String bookType) {
        this.age = age;
        this.bookType = bookType;
    }

    @Override
    public void registerAccount() {
        if (age > 12) {
            System.out.println("You have successfully registered under an Adult Account.");
        } else {
            System.out.println("Sorry, Age must be greater than 12 to register as an adult.");
        }
    }

    @Override
    public void requestBook(String bookType) {
        if ("Fiction".equalsIgnoreCase(bookType)) {
            System.out.println("Book Issued successfully, please return the book within 7 days.");
        } else {
            System.out.println("You are allowed to take only adult Fiction books.");
        }
    }
}

public class LibraryInterfaceDemo {

    public static void main(String[] args) {

        LibraryUser kidUser = new KidUser(10, "Kids");
        kidUser.registerAccount();
        kidUser.requestBook("Kids");

        LibraryUser kidUser2 = new KidUser(15, "Fiction");
        kidUser2.registerAccount();
        kidUser2.requestBook("Fiction");

        LibraryUser adultUser = new AdultUser(25, "Fiction");
        adultUser.registerAccount();
        adultUser.requestBook("Fiction");

        LibraryUser adultUser2 = new AdultUser(10, "Kids");
        adultUser2.registerAccount();
        adultUser2.requestBook("Kids");
    }
}
