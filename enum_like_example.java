public class Direction {
    private final String name;
    private final int degrees;
    
    // Private constructor
    private Direction(String name, int degrees) {
        this.name = name;
        this.degrees = degrees;
    }
    
    // Pre-defined constants
    public static final Direction NORTH = new Direction("North", 0);
    public static final Direction EAST = new Direction("East", 90);
    public static final Direction SOUTH = new Direction("South", 180);
    public static final Direction WEST = new Direction("West", 270);
    
    // Array of all directions
    private static final Direction[] VALUES = {NORTH, EAST, SOUTH, WEST};
    
    public String getName() {
        return name;
    }
    
    public int getDegrees() {
        return degrees;
    }
    
    public Direction opposite() {
        switch (this.name) {
            case "North": return SOUTH;
            case "South": return NORTH;
            case "East": return WEST;
            case "West": return EAST;
            default: return this;
        }
    }
    
    public static Direction[] values() {
        return VALUES.clone();
    }
    
    @Override
    public String toString() {
        return name + " (" + degrees + "°)";
    }
}

// Usage example
class EnumLikeExample {
    public static void main(String[] args) {
        // Cannot create new directions:
        // Direction custom = new Direction("Northeast", 45); // ERROR!
        
        // Can only use predefined constants
        Direction currentDirection = Direction.NORTH;
        System.out.println("Current direction: " + currentDirection);
        System.out.println("Opposite direction: " + currentDirection.opposite());
        
        System.out.println("\nAll directions:");
        for (Direction dir : Direction.values()) {
            System.out.println(dir);
        }
    }
}