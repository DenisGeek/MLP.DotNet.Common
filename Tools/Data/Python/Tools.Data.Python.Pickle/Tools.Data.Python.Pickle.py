#!/usr/bin/env python
import pickle

class DataPickle:
    def ReadFile(self, aFileName):
        #https://docs.python.org/3/library/pickle.html#examples
        with open(aFileName, 'rb') as f:
            # The protocol version used is detected automatically, so we do not
            # have to specify it.
            data = pickle.load(f)
            return data

#example
if __name__ == '__main__':
    data= DataPickle().ReadFile("")