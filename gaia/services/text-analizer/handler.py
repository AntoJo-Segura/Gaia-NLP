from __future__ import print_function

def text_analizer_handler(event, context):
    print(f'Text received: {event.Text}')