public class Main {

    static class Student {
        protected String name;
        protected String id;
        protected int age;
        protected double grade;
        protected String address;

        public Student() {}

        public Student(String name, String id, int age, double grade, String address) {
            this.name = name;
            this.id = id;
            this.age = age;
            this.grade = grade;
            this.address = address;
        }

        public String getName() { return name; }
        public void setName(String name) { this.name = name; }

        public String getId() { return id; }
        public void setId(String id) { this.id = id; }

        public int getAge() { return age; }
        public void setAge(int age) { this.age = age; }

        public double getGrade() { return grade; }
        public void setGrade(double grade) { this.grade = grade; }

        public String getAddress() { return address; }
        public void setAddress(String address) { this.address = address; }

        public void display() {
            System.out.println("Name: " + name);
            System.out.println("ID: " + id);
            System.out.println("Age: " + age);
            System.out.println("Grade: " + grade);
            System.out.println("Address: " + address);
        }

        public boolean isPassed() {
            return grade > 50;
        }
    }

    static class UGStudent extends Student {
        private String degree;
        private String stream;

        public UGStudent() {}

        public UGStudent(String name, String id, int age, double grade, String address, String degree, String stream) {
            super(name, id, age, grade, address);
            this.degree = degree;
            this.stream = stream;
        }

        public String getDegree() { return degree; }
        public void setDegree(String degree) { this.degree = degree; }

        public String getStream() { return stream; }
        public void setStream(String stream) { this.stream = stream; }

        @Override
        public void display() {
            System.out.println("UG Student Details:");
            super.display();
            System.out.println("Degree: " + degree);
            System.out.println("Stream: " + stream);
        }

        @Override
        public boolean isPassed() {
            return grade > 70;
        }
    }

    static class PGStudent extends Student {
        private String specialization;
        private int noOfPapersPublished;

        public PGStudent() {}

        public PGStudent(String name, String id, int age, double grade, String address, String specialization, int noOfPapersPublished) {
            super(name, id, age, grade, address);
            this.specialization = specialization;
            this.noOfPapersPublished = noOfPapersPublished;
        }

        public String getSpecialization() { return specialization; }
        public void setSpecialization(String specialization) { this.specialization = specialization; }

        public int getNoOfPapersPublished() { return noOfPapersPublished; }
        public void setNoOfPapersPublished(int noOfPapersPublished) { this.noOfPapersPublished = noOfPapersPublished; }

        @Override
        public void display() {
            System.out.println("PG Student Details:");
            super.display();
            System.out.println("Specialization: " + specialization);
            System.out.println("Number of Papers Published: " + noOfPapersPublished);
        }

        @Override
        public boolean isPassed() {
            return grade > 70 && noOfPapersPublished >= 2;
        }
    }

    public static void main(String[] args) {
        
        UGStudent ug = new UGStudent("Yuvaraj", "UG001", 25, 72.5, "Bangalore", "BSc", "Computer Science");
        ug.display();
        System.out.println("Passed: " + ug.isPassed());
        System.out.println();

        PGStudent pg = new PGStudent("Mukundhan", "PG002", 24, 75.0, "Chennai", "AI & ML", 3);
        pg.display();
        System.out.println("Passed: " + pg.isPassed());
        System.out.println();

        Student student = new Student("Unnikrishnan", "S003", 20, 45.0, "Delhi");
        student.display();
        System.out.println("Passed: " + student.isPassed());
    }
}
