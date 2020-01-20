# CS220 - Programming Assignment 1 : Boolean Logic
# author - Asa Ben-Hur and Nirmal Prajapati

# NOTE:
# You must use small single letters for your variable names, for eg. a, b, c
# You may use parenthesis to group your expressions such as 'a and (b or c)'

# Implement the following four functions:
# truth_table, count_satisfying, is_tautology and are_equivalent

# Submission:
# Submit this file using the checkin system on the course web page.



######## Do not modify the following block of code ########
# ********************** BEGIN *******************************

from functools import partial
import re
from itertools import product
#from truths import Truths


class Infix(object):
    def __init__(self, func):
        self.func = func
    def __or__(self, other):
        return self.func(other)
    def __ror__(self, other):
        return Infix(partial(self.func, other))
    def __call__(self, v1, v2):
        return self.func(v1, v2)

@Infix
def implies(p, q) :
    return not p or q

@Infix
def iff(p, q) :
    return (p |implies| q) and (q |implies| p)


# You must use this function to extract variables
# This function takes an expression as input and returns a sorted list of variables
# Do NOT modify this function
def extract_variables(expression):
    sorted_variable_set = sorted(set(re.findall(r'\b[a-z]\b', expression)))
    return sorted_variable_set

# *********************** END ***************************




############## IMPLEMENT THE FOLLOWING FUNCTIONS  ##############
############## Do not modify function definitions ##############


# This function calculates a truth table for a given expression
# input: expression
# output: truth table as a list of lists
# You must use extract_variables function to generate the list of variables from expression
# return a list of lists for this function
def truth_table(expression):
    vars = extract_variables(expression)
    table = product([True, False], repeat=len(vars))
    tr_tab = []
    for i in table:
        for j in range(len(vars)):
            exec(str(vars[j]) + '=' + str(i[j]))
            tab = list(i)
        tab.append(eval(expression))
        tr_tab.append(tab)
    return tr_tab


# count the number of satisfying values
# input: expression
# output: number of satisfying values in the expression
def count_satisfying(expression):
    count = 0
    table = truth_table(expression)
    for i in range(0,len(table)):
        if table[i][len(extract_variables(expression))] == True:
            count += 1
    return count

# if the expression is a tautology return True,
# False otherwise
# input: expression
# output: bool
def is_tautology(expression):
    table = truth_table(expression)
    for i in range(0,len(table)):
        if table[i][len(extract_variables((expression)))] == False:
            return False
    return True



# if expr1 is equivalent to expr2 return True,
# False otherwise
# input: expression 1 and expression 2
# output: bool
def are_equivalent(expr1, expr2):
    #table1 = truth_table((expr1))
    #table2 = truth_table((expr2))
    x = extract_variables(expr1)
    y = extract_variables(expr2)
    if (len(x).__eq__(y)):
        return True
    return False
    '''if (table1 == table2):
        return True
    return False'''

