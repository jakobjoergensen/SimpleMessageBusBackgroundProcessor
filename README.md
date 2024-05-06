# Simple In-Memory Message Bus With Background Processor

## TODO
- Retries for improved resilience
- Idempotent Consumer Pattern: Guarantee **At-Least-Once** message delivery
  1. Was the message already processed by the consumer?
  2. If *yes*, it's a duplicate. Do nothing.
  3. If *no*, handle the message.
  4. Store the message identifier: *Lazy idempotent consumer* or *Eager idempotent consumer*.

     **Lazy idempotent consumer**
       - For the happy path, handle the message and then store the message identifier.
       - If the handler throws an exception, do not store the message identifier.

      **Eager idempotent consumer**
        - Store the message identifier before handling the message.
        - If the handler throws an exception, remove the message identifier from the storage

- Dead Letter Queue
