public class Trie {

    // 27 Character Trie implementation, as the ? symbol is included at index 26

    string value;
    Trie[] children; 

    public Trie(string inputValue)
    {
        value = inputValue;
        children = new Trie[27];
    }

    public void AddChild(char newChild)
    {
        int newChildIndex = (newChild == '?' ? 26 : newChild - 97);

        children[newChildIndex] = new Trie(null);
    }

    public void GiveValue(string inputValue)
    {
        value = inputValue;
    }

    public static void InsertString(Trie trie, string insertedString, string newValue)
    {
        Trie temp = trie;

        if (insertedString.Equals("?"))
        {
            if (trie.children[26] == null)
                trie.children[26] = new Trie(null);
            temp = trie.children[26];
        }
        else
        {

            for (int i = 0; i < insertedString.Length; i++)
            {
                if (temp.children[insertedString[i] - 97] == null)
                    temp.children[insertedString[i] - 97] = new Trie(null);
                temp = temp.children[insertedString[i] - 97];
            }
        }

        temp.GiveValue(newValue);
    }

    public static string SearchForValue(Trie trie, string searchString)
    {
        Trie temp = trie;

        if (searchString.Equals("?")) {
            if (temp.children[26] == null)
                return null;
            return temp.children[26].value;
        }

        for (int i = 0; i < searchString.Length; i++)
        {
            if (temp.children[searchString[i] - 97] == null)
                return null;
            temp = temp.children[searchString[i] - 97];
        }

        return temp.value;
    }
}
