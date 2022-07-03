# AnalysisService

This service can be thought of as a API gateway for different analysis algorithms. Currently, there is only one analysis
algorithm used, namely OpenFace. In the future more algorithms are probably added or external services might be used.
This services job is to provide an API for choosing which algorithm must be run and what parameters it needs. It will
then trigger the chosen algorithm and track its progress.
