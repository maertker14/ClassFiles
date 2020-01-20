# CS220 - Programming Assignment 1 : Boolean Logic
# author - Nirmal Prajapati

# NOTE:
# This is a Sample Test file.
# This file is only for your reference.
# Do not submit this file.



import PA1

# This is the function we will use to check the format of your truth table
# Do not modify this function.
def check_format(table, num_vars):

    # check whether the table is a list
    assert type(table) is list, "table is not a list: %r" % table

    # check whether every row in the table is a list
    for row in table:
        assert type(row) is list, "table is not a list of lists: %r" % table

    # check if the table covers all possible combinations
    if len(table) < (2**num_vars):
        print('Truth table %r does not cover all combinations of variables. There should be %r combinations.' % (table, (2**num_vars)))
        return False

    # check whether table has extra rows
    if len(table) > (2**num_vars):
        print('Truth table %r has extra rows. There should be %r combinations.' % (table, (2**num_vars)))
        return False

    for row in table:
        # check if the table has missing columns
        if len(row) < (num_vars+1):
            print('One or more columns in the table are absent, total number of columns should be: %r' % len(row))
            return False
        # check if the table has extra columns
        elif len(row) > (num_vars+1):
            print('Truth table has extra columns, total number of columns should be: %r' % len(row))
            return False

    return True



# sample test cases for each function
# You should use different expressions to thoroughly test your output
def main():
    # sample test for truth_table function
    expression = 'a and b'
    tt = PA1.truth_table(expression)
    variables = PA1.extract_variables(expression)

    # check format of the truth table
    if check_format(tt, len(variables)):
        print('\nTruth table for the expression: ' + expression)
        print(tt)

    # sample test for count_satisfying function
    count = PA1.count_satisfying(expression)
    # check format of return value
    assert type(count) is int, "count_satisfying: return value is not an int" % count
    print('\nNumber of satisfying values in the expression \'' + expression + '\' is: ' + str(count))

    # sample test for is_tautology function
    is_a_tautology = PA1.is_tautology(expression)
    # check format of return value
    assert type(is_a_tautology) is bool, "is_tautology: return value is not a bool" % is_a_tautology
    if is_a_tautology:
        print('\nThe expression \'' + expression + '\' is a tautology!')
    else:
        print('\nThe expression \'' + expression + '\' is NOT a tautology!')

    # sample test for are_equivalent function
    expr1 = 'not a or b'
    expr2 = 'a |implies| b'

    tt1 = PA1.truth_table(expr1)
    variables1 = PA1.extract_variables(expr1)

    if check_format(tt1, len(variables1)):
        print('\nTruth table for expression 1: ' + expr1)
        print(tt1)

    tt2 = PA1.truth_table(expr2)
    variables2 = PA1.extract_variables(expr2)

    if check_format(tt2, len(variables2)):
        print('\nTruth table for expression 2: ' + expr2)
        print(tt2)

    are_equivalent_expressions = PA1.are_equivalent(expr1, expr2)
    assert type(are_equivalent_expressions) is bool, "are_equivalent: return value is not a bool" % are_equivalent_expressions
    if are_equivalent_expressions:
        print('\nThe expressions \'' + expr1 + '\' & \'' + expr2 + '\' are equivalent!')
    else:
        print('\nThe expressions \'' + expr1 + '\' & \'' + expr2 + '\' are NOT equivalent!')

if __name__ == '__main__':
    main()

