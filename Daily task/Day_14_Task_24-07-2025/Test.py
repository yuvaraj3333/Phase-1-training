def count_file_contents(filename):
    lines = 0
    words = 0
    characters = 0

    try:
        with open(filename, 'r', encoding='utf-8') as file:
            for line in file:
                lines += 1
                words += len(line.split())
                characters += len(line)
        print(f"Lines: {lines}")
        print(f"Words: {words}")
        print(f"Characters: {characters}")
    except FileNotFoundError:
        print(f"Error: The file '{filename}' does not exist.")
    except Exception as e:
        print(f"An error occurred: {e}")

if __name__ == "__main__":
    filename = input("Enter the filename: ")
    count_file_contents(filename)
