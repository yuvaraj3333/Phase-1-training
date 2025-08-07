import java.util.Scanner;

class Associate {
    private int associateId;
    private String associateName;
    private String workStatus;

    public Associate() {}

    public Associate(int associateId, String associateName) {
        this.associateId = associateId;
        this.associateName = associateName;
    }

    public int getAssociateId() {
        return associateId;
    }

    public void setAssociateId(int associateId) {
        this.associateId = associateId;
    }

    public String getAssociateName() {
        return associateName;
    }

    public void setAssociateName(String associateName) {
        this.associateName = associateName;
    }

    public String getWorkStatus() {
        return workStatus;
    }

    public void setWorkStatus(String workStatus) {
        this.workStatus = workStatus;
    }

    public void trackAssociateStatus(int days) {
        if (days <= 0) {
            this.workStatus = "Not started training";
        } else if (days <= 20) {
            this.workStatus = "Core skills";
        } else if (days <= 40) {
            this.workStatus = "Advanced modules";
        } else if (days <= 60) {
            this.workStatus = "Project phase";
        } else {
            this.workStatus = "Deployed in project";
        }
    }
}

public class AssociateTrainingTracker {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        System.out.print("Enter Associate ID: ");
        int id = sc.nextInt();
        sc.nextLine(); 

        System.out.print("Enter Associate Name: ");
        String name = sc.nextLine();

        System.out.print("Enter number of training days completed: ");
        int days = sc.nextInt();

        Associate associate = new Associate();
        associate.setAssociateId(id);
        associate.setAssociateName(name);

        associate.trackAssociateStatus(days);

        System.out.println("\n--- Associate Details ---");
        System.out.println("ID: " + associate.getAssociateId());
        System.out.println("Name: " + associate.getAssociateName());
        System.out.println("Work Status: " + associate.getWorkStatus());
    }
}
