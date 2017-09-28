# Command Pattern Calculator
### Overview
This project demonstrates a command line calculator which respects order of operations in it's inputs. User input is translated into commands which are sent by their client to a receiver which does the actual processing of numbers. The calculator supports standard operation (+, -, /, * ) some special operations (!, 1/x) as well as negative and decimal numbers. 

### Usage
**Allowed Input:**
* Numbers: Doubles including negatives
* Operators: +, -, *, \/
* Special Operators: 1/x, !
* Functions: 
  * 'A': All Clear
  * 'C': Clear command
  * '=': solve
  * 'Q': Quit
  
**Example Workflow:**
1) Build and run the program
2) In the command line begin typing your expression to be evaluated
3) When it is time to evaluate, simply press the '=' key
4) The solution will be returned on the next line. Use an operator to continue using that value or the 'A' key followed by '=' to clear the program.
5) To clear the last command you entered simply add a 'C' to your expression. This will disregard the last operator you entered or the last full command. ie: 5 + 20C + 3 = 8 AND 5 + C + 3 = 8
6) Enter 'Q' to quit

### Assumptions & Design Decisions
* **Command Casing:** All commands are case sensitive. All letters must be in their uppercase form. So 'a' is invalid but 'A' will perform a Clear All operation.
* **Clear Command:** The function of the clear command was not clearly defined in the spec. The assumption is made that when a 'C' is entered in an expression the last command, whether it was completed or not is removed. For an example see step 5 of **Usage** above.
* **All Clear Command:** The All Clear command clears the queue of inputted commands as well as the internal system value meaning an all clear command always returns the calculator to a value of 0.
* **User Input Error Handling:** When a user enters erroneous input the system clears it's internal memory including disregarding any previous commands. It will then sit and wait for user input to begin again. 
* **Factorials of Negatives:** Factorials of negative numbers return an error. A later version of the calculator could implement the gamma functions to handle this case. 

### Testing Strategy
Since the command pattern utilizes methods making calls against other surfaces, testing has to rely heavily on checking state changes as opposed to return values. When possible mocks that implement interfaces of pieces of the system were used to isolate functionality to the unit under test. Most of the tests then, follow a pattern of perform an action then assert the state of the receiving object was updated correctly.

### Frameworks Used
* MSTest

### Future Improvements
This was my first time venturing into Test Driven Deployment and command pattern and absolutely loved working with them! I actually want to continue iterating on this project as a means to enrich myself on these two concepts. Regardless of the decision to move forward, I would really appreciate any feedback on my implementation of TDD and Command Pattern. Any thoughts will be immensely appreciated. Thank you!