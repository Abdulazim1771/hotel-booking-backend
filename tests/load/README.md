# Load tests

The first k6 scenario will target availability at 100 requests per second for five minutes on a local or dedicated test environment. Compare one and two API replicas using the same seeded dates and room inventory. Track dropped iterations as well as latency and errors.

Measure last-room correctness separately with the PostgreSQL integration test. Store workload details and observed results in the [performance reports](../../docs/performance/README.md).
