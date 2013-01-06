import clr

clr.AddReferenceByPartialName("System")
clr.AddReferenceByPartialName("System.Messaging")

from System.Messaging import MessageQueue

def deleteAllLocalPrivateQueues():
	queues = MessageQueue.GetPrivateQueuesByMachine(".")
	for queueIndex in range(0,len(queues)):
		queue = queues[queueIndex]
		MessageQueue.Delete(queue.Path)

def purgeAllLocalPrivateQueues():
	queue = MessageQueue.GetPrivateQueuesByMachine(".")
	for queueIndex in range(0,len(queues)):
		queue = queues[queueIndex]
		queue.Purge()
