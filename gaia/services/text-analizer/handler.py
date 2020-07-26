from __future__ import print_function
from liquid import Liquid
from Levenshtein import distance
# from serverless_event_mocks import create_event # 
# from functools import *
from numpy import argmax


class event:
    def __init__(self):
        self.Text = "Ready, are you? What know you of ready? For eight hundred years have I trained Jedi. My own counsel will I keep on who is to be trained! A Jedi must have the deepest commitment, the most serious mind. This one a long time have I watched. Never his mind on where he was. Hmm? What he was doing. Hmph. Adventure. Heh! Excitement. Heh! A Jedi craves not these things. You are reckless!"


def text_analizer_handler(event, context):
    cpv_dic = {"1":"word1", "2": "word2"} #dynamo here
    full_split_text = event.Text.split(' ')
    # print('Text received: ' + event.Text) #Dani remember
    # for x in event.Text.split(' '):print(x.replace(',',''))
        

    # def distance(word1:  str, word2: str ) -> int:
    #     return len(word1) - len(word2)
        
    #actualy it returns one result for each cpv, it should return more than one  
    def full_text_metric(x: str) -> [int]:
        
        similarity_chain = list(map( #equal to list(map())
                lambda w: distance(x,w),
                 full_split_text
             )
        )
        
        maxval = max(
            similarity_chain
        )
        
        maxpos = argmax(similarity_chain)
        
        return maxval,maxpos
    
        
    result = dict(zip(
        map( #text position of max similarity
            full_text_metric,
            cpv_dic.values()
        ),
        cpv_dic.values()
    ))
    
    print(result.keys())
    print(result.values())

test_event = event()
text_analizer_handler(test_event , '')