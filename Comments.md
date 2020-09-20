1. IProduct interface is a bit implicit. 
2  Implementation logic is all in one place.
3  One can not test handlers independently.
4  Some interfaces just add duplication to logic.
5  May be it is better to make requests immutable to make sure they are not changed while processing.
 