public class MathUtils {
    // Private constructor prevents instantiation
    private MathUtils() {
        throw new AssertionError("Utility class should not be instantiated");
    }
    
    // All methods are static
    public static int add(int a, int b) {
        return a + b;
    }
    
    public static int multiply(int a, int b) {
        return a * b;
    }
    
    public static double calculateCircleArea(double radius) {
        return Math.PI * radius * radius;
    }
    
    public static boolean isPrime(int number) {
        if (number <= 1) return false;
        for (int i = 2; i <= Math.sqrt(number); i++) {
            if (number % i == 0) return false;
        }
        return true;
    }
}

// Usage example
class UtilityExample {
    public static void main(String[] args) {
        // This would cause compilation error:
        // MathUtils utils = new MathUtils(); // ERROR!
        
        // Correct usage - call static methods directly
        int sum = MathUtils.add(5, 3);
        int product = MathUtils.multiply(4, 7);
        double area = MathUtils.calculateCircleArea(5.0);
        boolean isPrime = MathUtils.isPrime(17);
        
        System.out.println("Sum: " + sum);
        System.out.println("Product: " + product);
        System.out.println("Circle area: " + area);
        System.out.println("Is 17 prime? " + isPrime);
    }
}