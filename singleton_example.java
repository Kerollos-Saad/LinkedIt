public class DatabaseConnection {
    // Static instance variable
    private static DatabaseConnection instance;
    
    // Private constructor prevents external instantiation
    private DatabaseConnection() {
        // Initialize database connection
        System.out.println("Database connection established");
    }
    
    // Public static method to get the single instance
    public static DatabaseConnection getInstance() {
        if (instance == null) {
            instance = new DatabaseConnection();
        }
        return instance;
    }
    
    public void executeQuery(String query) {
        System.out.println("Executing query: " + query);
    }
}

// Usage example
class Main {
    public static void main(String[] args) {
        // This would cause compilation error:
        // DatabaseConnection db = new DatabaseConnection(); // ERROR!
        
        // Correct way to get instance
        DatabaseConnection db1 = DatabaseConnection.getInstance();
        DatabaseConnection db2 = DatabaseConnection.getInstance();
        
        // Both references point to the same object
        System.out.println(db1 == db2); // true
        
        db1.executeQuery("SELECT * FROM users");
    }
}