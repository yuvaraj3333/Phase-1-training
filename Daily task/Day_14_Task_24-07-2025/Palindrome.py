def is_palindrome(text):
    cleaned_text = text.replace(" ", "").lower()
    length = len(cleaned_text)

    for i in range(length // 2):
        if cleaned_text[i] != cleaned_text[length - 1 - i]:
            return False
    return True

def main():
    userinput = input("Enter a string: ")

    if is_palindrome(userinput):
        print("The string is a palindrome")
    else:
        print("The string is not a palindrome")

if __name__ == "__main__":
    main()
